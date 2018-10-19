import './scss/app.scss'

import React from "react";
import ReactDOM from "react-dom";
import Header from './components/Header'
import Loader from './components/Loader'

class HelloMessage extends React.Component {
    render() {
        return <div>
            <Header/>
            <img className="img-fluid" src="https://wccstores.blob.core.windows.net/images/dark-1845065.jpg" alt="Chania"></img>
            <Loader/>
            <div className="container">
                <h1>Hello {this.props.name}</h1>
            </div>
        </div>
    }
}

let App = document.getElementById("app");

ReactDOM.render(<HelloMessage name="Yomi" />, App);