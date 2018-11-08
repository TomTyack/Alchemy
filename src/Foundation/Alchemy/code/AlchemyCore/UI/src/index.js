import './scss/app.scss'

import React from "react";
import ReactDOM from "react-dom";
import Header from './components/Header'
import Loader from './components/Loader'
import GoButton from './components/GoButton'
import ProgressBar from './components/ProgressBar'
import InteractiveBoard from './components/InteractiveBoard'

import {AlchemyContext} from './components/AlchemyContext';

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
            console.log("toggle scanning " );
            this.setState(state => ({
                scanning: true,
                waiting: false
              }));
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
                    
                    <GoButton id="goBtn" visible={this.state.waiting} />

                    <ProgressBar id="progressBar" visible={this.state.scanning} />

                    <InteractiveBoard id="InteractiveBoard" visible={this.state.scanning} />
                </div>
            </div>
        </AlchemyContext.Provider>);
    }
}

let App = document.getElementById("app");

ReactDOM.render(<AlchemyApp name="AlchemyApp" />, App);