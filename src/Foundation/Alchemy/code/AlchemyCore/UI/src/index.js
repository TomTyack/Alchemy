import './scss/app.scss'

import React from "react";
import ReactDOM from "react-dom";
import Header from './components/Header'
import Loader from './components/Loader'
import GoButton from './components/GoButton'
import ProgressBar from './components/ProgressBar'
import {RuleDataService} from './components/data/RuleDataService';
import PropTypes from 'prop-types'
import {AlchemyContext} from './components/AlchemyContext';
import {RulesToasterAlerts} from './components/RulesToasterAlerts';


class AlchemyApp extends React.Component {

    constructor(props) {
        super(props);
        this.registerEvents()
        this.state = {
            loading: true, 
            waiting: false,
            toggleWaiting : this.toggleWaiting,
            toggleLoading : this.toggleLoading,
            scanning: false,
            startScanning : this.startScanning,
        };
      }

      /*
      * Register global broadcast function that nested components can use to change global state.
      */
      registerEvents() {
        this.toggleLoading = (val) => {
            this.setState(state => ({
                loading: val
              }));
          };
          this.startScanning = () => {
            console.log("start scanning");
            var ruleDataService = new RuleDataService();

            this.setState(state => ({
                scanning: true,
                waiting: false
            }));

            let rulesPromise = ruleDataService.beginProcessingRules();
            
            rulesPromise.then(function (result) {
                this.props.data = result;
                //thisClass.triggerIncrementally(thisClass.props.data.rules.length, null);
            });

          };

          // The waiting state, show the Go button ready to go
          this.toggleWaiting = (val) => {
            this.setState(state => ({
                waiting: val
              }));
          };
    }  

    render() {
        return (
        <AlchemyContext.Provider value={this.state}>
            <div>
                <Header/>
                <img className="img-fluid" src="https://wccstores.blob.core.windows.net/images/dark-1845065.jpg" alt="Chania"></img>
                <div>                    
                    <AlchemyContext.Consumer>
                        {({toggleLoading, toggleWaiting}) => (
                            <Loader id="mainLoader" toggleLoading={toggleLoading} toggleWaiting={this.toggleWaiting}/>
                        )}
                    </AlchemyContext.Consumer>
                    
                    <GoButton id="goBtn" visible={true} />

                    <ProgressBar id="progressBar" visible={false} />
                    
                    <RulesToasterAlerts id="rulesToaster" visible={this.scanning} />

                </div>
            </div>
        </AlchemyContext.Provider>);
    }
}

AlchemyApp.propTypes = {
    data: PropTypes.object
};

AlchemyApp.defaultProps = {
    data: {},
    rulesLoaded: false
};

let App = document.getElementById("app");

ReactDOM.render(<AlchemyApp name="AlchemyApp" />, App);