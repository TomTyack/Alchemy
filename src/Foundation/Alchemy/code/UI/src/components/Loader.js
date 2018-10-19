import './../scss/loader.scss'

// Base React modules.
const React = require('react');
const createReactClass = require('create-react-class');
const PropTypes = require('prop-types');

// Reactstrap components.
const Modal = require('reactstrap').Modal;

const Loader = createReactClass({

	/**
	 * Define the component's properties and their PropTypes.
	 * @type {Object}
	 */
	propTypes: {
		data: PropTypes.object,
		btn: PropTypes.string,
		htmlcontent: PropTypes.bool
	},

	/**
	 * Define the component's default properties pre-data.
	 * @type {Object}
	 */
	defaults: {
		"component": {
			"theme": "light"
		}
	},

	/**
	 * Define the default component state.
	 * @return {Object} The default `this.state` object.
	 */
	getInitialState() {
		return {
			mobile: false,
			modal: false
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
		window.removeEventListener('resize', this.handleMobileChange);
	},

	/**
	 * Render the component to the ReactDOM.
	 * @return {Object} JSX Expression.
	 */
	render() {
		return (
			<div className="container">
                <div className="loader">
                <div className="loader--dot"></div>
                <div className="loader--dot"></div>
                <div className="loader--dot"></div>
                <div className="loader--dot"></div>
                <div className="loader--dot"></div>
                <div className="loader--dot"></div>
                <div className="loader--text"></div>
                </div>
            </div>
		);
	}
});

module.exports = Loader;
