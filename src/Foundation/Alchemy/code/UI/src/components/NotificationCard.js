import './../scss/card.scss'

// Base React modules.
const React = require('react');
const createReactClass = require('create-react-class');
const PropTypes = require('prop-types');

import { ToastContainer, ToastMessageAnimated, ToastMessageAnimatedProps  } from "react-toastr";

const NotificationCard = createReactClass({

	/**
	 * Define the component's properties and their PropTypes.
	 * @type {Object}
	 */
	propTypes: {
		visible: PropTypes.bool
	},

	/**
	 * Define the component's default properties pre-data.
	 * @type {Object}
	 */
	defaults: {
		visible: true
	},

	/**
	 * Define the default component state.
	 * @return {Object} The default `this.state` object.
	 */
	getInitialState() {
		return {
			visible: true,
			active: false
		};
	},

	getDefaultProps() {
		return {
		  
		  visible: true
		};
	  },

	container: {},

	setContainer(ref) {
		this.container = ref;

		this.container.success(`hi! Now is ${new Date()}`, `///title\\\\\\`, {
			closeButton: true,
		  })
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

	/**
	 * Render the component to the ReactDOM.
	 * @return {Object} JSX Expression.
	 */
	render() {
		let result = (null);

		const ToastMessageFactory = React.createFactory(ToastMessageAnimated);

		ToastMessageFactory.props.timeOut = 50000;

		if(this.props.visible)
		{
			result = (
				<div className="alchemy-card">

					<ToastContainer ref={ref => this.setContainer(ref)} className="toast-top-right"
					toastMessageFactory={ToastMessageFactory}
					/>

				</div>
				);
		}

		return result;		
	}
});

module.exports = NotificationCard;
