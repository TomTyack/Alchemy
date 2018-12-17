// Base React modules.
const React = require('react');
const createReactClass = require('create-react-class');
const PropTypes = require('prop-types');

const RulesListing = createReactClass({


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

	/**
	 * Render the component to the ReactDOM.
	 * @return {Object} JSX Expression.
	 */
	render() {
		let result = (null);

		if(this.props.visible)
		{
			result = (
                <div className="rules-dashboard">
                </div>
            );
		}

		return result;		
	}
});

module.exports = RulesListing;
