
import axios from 'axios';

export class RuleDataService  {

    constructor() {
    }

    ReadRules() {
        const request = axios.get(`http://sc827/alchemy/api/rules/ruleslist/`);
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
                let rules = thisClass.formatRulesData(result);
                resolve(rules);
            });
        });

        return promise;
    }

    formatRulesData(result){

		let ruleSet = [];
		let rules = [];
		ruleSet.push(result.data);

		for (let i = 0; i< ruleSet.length; i++) {
            console.log(ruleSet);
            if(ruleSet)
            {
                console.log(typeof ruleSet)
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
        return rules;
	}
}
