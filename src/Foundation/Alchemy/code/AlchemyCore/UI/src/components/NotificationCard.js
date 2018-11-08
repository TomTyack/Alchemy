import './../scss/card.scss'

// Base React modules.
const React = require('react');
const createReactClass = require('create-react-class');
const PropTypes = require('prop-types');

import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

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

	notify (){
		toast("Wow so easy !");
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

		if(this.props.visible)
		{
			result = (
				<div className="alchemy-card">

					<button onClick={this.notify}>Notify !</button>
          			<ToastContainer />

				</div>
				);
		}

		return result;		
	}
});

module.exports = NotificationCard;
