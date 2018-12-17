import './../scss/go.scss'

// Base React modules.
const React = require('react');
const createReactClass = require('create-react-class');
const PropTypes = require('prop-types');

import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const RulesToasterAlerts = createReactClass({

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
            visible: false
		};
	},

	/**
	 * Define the default component state.
	 * @return {Object} The default `this.state` object.
	 */
	getInitialState() {
		return {
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
	componentWillUnmount() {
    },

	kickOffRules(){
		let thisClass = this;
		thisClass.triggerIncrementally(thisClass.props.data.rules.length, null);
    },
     
    triggerIncrementally : function theLoop (i, aClass) {
		let thisClass = this;
		if(!thisClass)
			thisClass = aClass;
		setTimeout(function () {
            thisClass.triggerToast(thisClass.props.data.rules[i-1]);
            if (--i) {                  // If i > 0, keep going
                theLoop(i, thisClass);  // Call the loop again
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
		if(this.props.visible)
		{
            if(this.props.data.rules && this.props.data.rules.length > 0)
            {
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
		}
		return (null);		
	}
});

module.exports = RulesToasterAlerts;
