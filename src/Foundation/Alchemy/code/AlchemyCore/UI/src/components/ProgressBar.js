import './../scss/loader.scss'

// Base React modules.
const React = require('react');
const createReactClass = require('create-react-class');
const PropTypes = require('prop-types');

import {AlchemyContext} from './AlchemyContext';

const ProgressBar = createReactClass({

	/**
	 * Define the component's properties and their PropTypes.
	 * @type {Object}
	 */
	propTypes: {
		data: PropTypes.object,
		visible: PropTypes.bool,
	},

	/**
	 * Define the component's default properties pre-data.
	 * @type {Object}
	 */
	defaults: {
	},

	/**
	 * Define the default component state.
	 * @return {Object} The default `this.state` object.
	 */
	getInitialState() {
		return {
			visible: false,
			active: false
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

	/**
	 * Render the component to the ReactDOM.
	 * @return {Object} JSX Expression.
	 */
	render() {
		if(this.props.visible)
		{
			let progress = { width : "40%" };

			return (
				<div id="alchemyScanProgress">
					<AlchemyContext.Consumer>
						{({startScanning}) => (
							<div className="progress">
								<div className="progress-bar progress-bar-striped progress-bar-animated" style={progress}></div>
							</div>
						)}
					</AlchemyContext.Consumer>
				</div>
				);
		}
		return (null);		
	}
});

module.exports = ProgressBar;
