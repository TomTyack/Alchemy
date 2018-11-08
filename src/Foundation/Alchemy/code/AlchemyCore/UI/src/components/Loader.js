import './../scss/loader.scss'

// Base React modules.
const React = require('react');
const createReactClass = require('create-react-class');
const PropTypes = require('prop-types');

// Reactstrap components.

const Loader = createReactClass({

	/**
	 * Define the component's properties and their PropTypes.
	 * @type {Object}
	 */
	propTypes: {
		data: PropTypes.object,
		toggleLoading : PropTypes.func,
		toggleWaiting : PropTypes.func,
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
			visible: true
		};
	},

	/**
	 * Method called when component has mounted successfully to the ReactDOM.
	 * @return {null} Method doesn't return a value.
	 */
	componentDidMount() {

		this.timer = setInterval(() => { 
			onceOff();
			} 
			, 5000);

			let onceOff = () => {
				this.setState(prevState => ({ visible: false })); 
				this.props.toggleWaiting(true);
				clearInterval(this.timer);
			};
	},

	/**
	 * Method called when component will successfully unmount from the ReactDOM.
	 * @return {null} Method doesn't return a value.
	 */
	componentWillUnmount() {
	},

	/**
	 * Render the component to the ReactDOM.
	 * @return {Object} JSX Expression.
	 */
	render() {
		if(this.state.visible)
		{
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
		}else {
			return (null);
		}
		
	}
});

module.exports = Loader;
