import './../scss/go.scss'

// Base React modules.
const React = require('react');
const createReactClass = require('create-react-class');
const PropTypes = require('prop-types');

import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const RulesToasterAlerts = createReactClass({

	/**
	 * Define the default component state.
	 * @return {Object} The default `this.state` object.
	 */
	getInitialState() {
		return {
			rules: this.props.dataService.ruleAlertsPending
		};
	},

	propTypes: {
		data: PropTypes.object,
		dataService: PropTypes.object,
		visible: PropTypes.bool
	},

	/**
	 * Define the component's default properties pre-data.
	 * @type {Object}
	 */
	getDefaultProps: function() {
		return {
			data: {
				runComplete: false,
				rulesKickedOff: false
			},
			dataService: {},
			visible: false
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
	componentWillUnmount() {
	},
	
	componentDidUpdate(prevProps) {
		// Typical usage (don't forget to compare props):
		if (!this.props.data.runComplete && this.props.dataService && this.props.dataService.ruleAlertsPending && this.props.dataService.ruleAlertsPending.length > 0) {
			let pendingArray = this.props.dataService.ruleAlertsPending.slice(0);

			this.setState({
				rules: pendingArray
			});
			this.props.dataService.ruleAlertsPending = [];
		}
	},

	kickOffRules(){
		let thisClass = this;
		if(thisClass.state.rules && thisClass.state.rules.length > 0)
		{
			this.props.data.rulesKickedOff = true;
			thisClass.triggerIncrementally(thisClass.state.rules.length, null);
		}
    },
     
    triggerIncrementally : function theLoop (i, aClass) {
		let thisClass = this;
		if(!thisClass)
			thisClass = aClass;
		setTimeout(function () {
            thisClass.triggerToast(thisClass.state.rules[i-1]);
            if (--i && i>0) {                  // If i > 0, keep going
                theLoop(i, thisClass);  // Call the loop again
			}else
			{
				thisClass.props.data.runComplete = true;
			}
		}, 1500);
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
		  toast(rule.Name);		
	},

	initialToastClosed(rule){
		let thisClass = this;
		thisClass.addRule(rule);
	},

	/**
	 * Render the component to the ReactDOM.
	 * @return {Object} JSX Expression.
	 */
	render() {
		if(this.props.visible && !this.props.data.runComplete)
		{
			if(!this.props.data.rulesKickedOff)
				this.kickOffRules();

			return (
				<div>
					<div id="interactive-board">
						<div className={"container"}>
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
						</div>					
					</div>
				</div>
				);
		}
		return (null);		
	}
});

module.exports = RulesToasterAlerts;
