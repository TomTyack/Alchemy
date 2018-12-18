
import axios from 'axios';

export class RuleDataService  {

    constructor() {
        this.rulesStarted = [];
        
        
        this.ruleAlertsPending = [];// Used to show new toast alerts
        this.dashboardPending = []; // Used to show dashboard information blocks for completed rules

        this.rulesCompleted = []; // Contains all rules finished processing, success or failure
        this.rulesFailed = [];
        this.rulesSucceeded = [];

        this.updateRuleListing = null;
    }

    ENDPOINTURL() {
        return "http://sc827/";
    }

    ReadRules() {
        const request = axios.get(this.ENDPOINTURL()+ `alchemy/api/rules/ruleslist/`);
        return request.then(res => {
            return res;
        });
    }

    ReadRule(ruleID) {
        const request = axios.get(this.ENDPOINTURL()+ `alchemy/api/rules/run/` + ruleID);
        return request.then(res => {
            return res;
        });
    }

    beginProcessingRules()
    {
        let rulesPromise = this.ReadRules();
        let thisClass = this;
        
        var promise = new Promise(function(resolve, reject) {
            rulesPromise.then(function (result) {
                thisClass.formatRulesData(result);
                resolve(true);
            });
        });

        return promise;
    }

    formatRulesData(result){

		let ruleSet = [];
		let rules = [];
		ruleSet.push(result.data);

		for (let i = 0; i< ruleSet.length; i++) {
            if(ruleSet)
            {
                let rulesStop = ruleSet[i];
                for (let j = 0; j<rulesStop.length; j++) {
                    let whoKnows = rulesStop[j];
                    for (let key in whoKnows) {
                                            
                        if (whoKnows.hasOwnProperty(key)) {
                            let thisRule = whoKnows[key];
                            thisRule.key = thisRule.UniqueId;
                            rules.push(thisRule);
                        }
                    }
                }
            }			
        }
        this.rulesStarted = [];
        this.rulesStarted =  rules.slice(0);
        this.ruleAlertsPending = [];
        this.ruleAlertsPending = rules.slice(0); // track these to show the initial alerts
    }
    
    /**
     * Kicks off a polling event to check for rules waiting from completion
     * @param {*} data 
     */
    beginPollingRunningRules(aClass)
    {
        let thisClass = this;
        if(aClass)
            thisClass = aClass;
        setTimeout(function () {
            if(thisClass.rulesStarted.length != 0)
            {
                thisClass.verifyRulesWaitingCompletion(thisClass);
                thisClass.beginPollingRunningRules(thisClass);
            }
        }, 5000);
    }

    /**
     * Verify that the a particular rule is still running
     * @param {*} data 
     */    
    verifyRulesWaitingCompletion(thisClass)
    {
        for (var i = 0; i < thisClass.rulesStarted.length; i++){
            var obj = thisClass.rulesStarted[i];
            thisClass.VerifyRulesStatus(obj, thisClass);
        }
    }

    /**
     * Given a rule call the API and verify if the rules has completed processing.
     * @param {*} rule 
     */
    VerifyRulesStatus(originalRules, thisClass){
        let rulesPromise = thisClass.ReadRule(originalRules.Id);
        
        rulesPromise.then(function (rule) {

            let match = thisClass.rulesStarted.find((element) => {
                return element.Id === originalRules.Id;
            });

            if(match && rule.data)
            {
                var index = thisClass.rulesStarted.map(x => {
                    return x.Id;
                }).indexOf(originalRules.Id);
                
                thisClass.rulesStarted.splice(index, 1);

                thisClass.rulesCompleted.push(match);
                
                if(rule.data.Success)
                {
                    match.Success = true;
                    thisClass.rulesSucceeded.push(match);
                }else if (rule.data && !rule.data.Success)
                {
                    match.Success = false;
                    thisClass.rulesSucceeded.push(match);
                }
                thisClass.dashboardPending.push(match);
                thisClass.updateRuleListing();
            }
        });
    }

    SetEvents(updateRuleListing){
        this.updateRuleListing = updateRuleListing;
    }
}
