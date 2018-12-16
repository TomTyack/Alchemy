import './../scss/go.scss'

// Base React modules.
const React = require('react');
const createReactClass = require('create-react-class');
const PropTypes = require('prop-types');

import NotificationCard from './NotificationCard';

import {AlchemyContext} from './AlchemyContext';

import {RuleDataService} from './data/RuleDataService';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { Fade, Stagger } from 'react-animation-components'

const InteractiveBoard = createReactClass({

	/**
	 * Define the component's properties and their PropTypes.
	 * @type {Object}
	 */
	propTypes: {
		data: PropTypes.object,
		visible: PropTypes.bool
	},

	/**
	 * Define the component's default properties pre-data.
	 * @type {Object}
	 */
	getDefaultProps: function() {
		return {
			data: {},
			rulesLoaded: false
		};
	  },

	/**
	 * Define the default component state.
	 * @return {Object} The default `this.state` object.
	 */
	getInitialState() {
		return {
			visible: false,
			active: false,
			rules: []
		};
	},

	/**
	 * Method called when component has mounted successfully to the ReactDOM.
	 * @return {null} Method doesn't return a value.
	 */
	componentDidMount() {
		
	},

	/**
	 * Method called when component will successfully unmount from the ReactDOM.
	 * @return {null} Method doesn't return a value.
	 */
	componentWillUnmount() {},

	kickOffRules(){

		// if(this.props.data.rulesLoaded)
		// 	return;

		var ruleDataService = new RuleDataService();
		let rulesPromise = ruleDataService.ReadRules();
		let thisClass = this;

		if(!thisClass.props.data)
		{
			thisClass.props.data = {};
		}
		
		rulesPromise.then(function (result) {
			thisClass.beginRuleProcessing(result);
			thisClass.props.data.rulesLoaded = true;
			thisClass.triggerIncrementally(thisClass.props.data.rules.length, null);
		});

		let delayInMilliseconds = 5000; //1 second

		// setTimeout(function() {
		// 	thisClass.props.data.rules.forEach(thisRule => {
		// 		setTimeout(function () {
		// 			thisClass.triggerToast(thisRule);
		// 		}, 3000);
				
		// 	});
		// }, delayInMilliseconds);
		
		
	}, triggerIncrementally : function theLoop (i, aClass) {
		let thisClass = this;
		if(!thisClass)
			thisClass = aClass;
		setTimeout(function () {
		  //alert("Cheese!");
		  
		  thisClass.triggerToast(thisClass.props.data.rules[i-1]);
		  // DO SOMETHING WITH data AND stuff
		  if (--i) {                  // If i > 0, keep going
			theLoop(i, thisClass);  // Call the loop again
		  }
		}, 3000);
	  },

	beginRuleProcessing(result){

		let thisClass = this;

		thisClass.props.data.ruleSet = [];
		thisClass.props.data.rules = [];
		thisClass.props.data.ruleSet.push(result.data);

		for (let i = 0; i<thisClass.props.data.ruleSet.length; i++) {
			let ruleSet = thisClass.props.data.ruleSet[i];
			for (let j = 0; j<ruleSet.length; j++) {
				let whoKnows = ruleSet[j];	
				for (let key in whoKnows) {
										
					if (whoKnows.hasOwnProperty(key)) {
						let thisRule = whoKnows[key];
						thisRule.key = thisRule.UniqueId;
						thisClass.props.data.rules.push(thisRule);
						//thisClass.triggerToast(thisRule);
					}
				}
			}
		}
	},

	wait(ms){
		let start = new Date().getTime();
		let end = start;
		while(end < start + ms) {
		  end = new Date().getTime();
	   }
	 },

	triggerToast(rule){
		let thisClass = this;
		const options = {
			toastId: rule.UniqueId,
			//onOpen: props => console.log(props.foo),
			onClose: props => thisClass.initialToastClosed(rule),
			// autoClose: 6000,
			// closeButton: <FontAwesomeCloseButton />,
			// type: toast.TYPE.INFO,
			// hideProgressBar: false,
			// position: toast.POSITION.TOP_LEFT,
			// pauseOnHover: true,
			// transition: MyCustomTransition,
			// and so on ...
		};

		const Msg = ({ closeToast }) => (
			<div>			
				<div className="micro">
				Starting: 
			</div>
			<div>
				{rule.Name}
			</div>
		  </div>
		  )
		//   this.wait(20000);
		  //toast(<Msg />, options);
		  toast(rule.Name);	
		  		
		// toast("Custom Style Notification with css class!", {
		// 	position: toast.POSITION.BOTTOM_RIGHT,
		// 	className: 'foo-bar'
		//   });	
	},

	initialToastClosed(rule){
		let thisClass = this;
		thisClass.addRule(rule);
	},

	renderRules(){	
		//const items = [rule.Name, 'second', 'third', 'fourth', 'fifth'];
		let stagger = (
			this.state.rules.map(
				item => (
					<Stagger in>
						<Fade>
							<div>Each {item.Name} will transition in with an incrementally larger delay than the previous</div>
						</Fade>
					</Stagger>
				)
			)
		);
		return stagger;
	},

	addRule(rule) {

		  this.setState((prevState) => {
			return { 
			  rules: prevState.rules.concat(rule) 
			};
		  });
		 		 
		 console.log(this.state.rules);
	  },

	  notify() {
		toast("Default Notification !");
  
		toast.success("Success Notification !", {
		  position: toast.POSITION.TOP_CENTER
		});
  
		toast.error("Error Notification !", {
		  position: toast.POSITION.TOP_LEFT
		});
  
		toast.warn("Warning Notification !", {
		  position: toast.POSITION.BOTTOM_LEFT
		});
  
		toast.info("Info Notification !", {
		  position: toast.POSITION.BOTTOM_CENTER
		});
  
		toast("Custom Style Notification with css class!", {
		  position: toast.POSITION.BOTTOM_RIGHT,
		  className: 'foo-bar'
		});
	  },

	/**
	 * Render the component to the ReactDOM.
	 * @return {Object} JSX Expression.
	 */
	render() {
		if(this.props.visible)
		{
			this.kickOffRules();

			return (
				<div>
					<div className="overlay">
						{this.renderRules()}
					</div>
					<div id="interactive-board">
						<div className={"container"}>
							<NotificationCard visible={true} />
							<ToastContainer
position="top-right"
autoClose={4998}
hideProgressBar={false}
newestOnTop
closeOnClick
rtl={false}
pauseOnVisibilityChange
draggable
pauseOnHover
/>
							<button onClick={this.notify}>Notify</button>
						</div>					
					</div>
				</div>
				);
		}
		return (null);		
	}
});

module.exports = InteractiveBoard;
