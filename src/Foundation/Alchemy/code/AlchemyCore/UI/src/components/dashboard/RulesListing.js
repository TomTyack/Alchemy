// Base React modules.
const React = require('react');
const createReactClass = require('create-react-class');
const PropTypes = require('prop-types');
const { PoseGroup, posed } = require('react-pose');


const ItemList = ({ items }) => (
	<ul>
	  <PoseGroup>
		{items.map(item => <StyledItem key={item.Id}>{item.Name}</StyledItem>)}
	  </PoseGroup>
	</ul>
);

const RulesListing = createReactClass({

	// state = {
	// 	rules: this.props.initialX
	// },

	/**
	 * Define the default component state.
	 * @return {Object} The default `this.state` object.
	 */
	getInitialState() {
		return {
			rules: []
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
			data: {},
			dataService: {},
            visible: false
		};
	},

	componentDidUpdate(prevProps) {
		// Typical usage (don't forget to compare props):
		if (this.props.dataService && this.props.dataService.dashboardPending && this.props.dataService.dashboardPending.length > 0) {
			
			// Construct an array with all pending items
			let pendingArray = this.props.dataService.dashboardPending.slice(0);

			if(this.state.pendingArray)
				pendingArray.push(this.state.pendingArray); // Add existing items already in the state

			this.props.dataService.dashboardPending = [];
			this.setState({
				rules: pendingArray
			});
		}
	},

	/**
	 * Method called when component has mounted successfully to the ReactDOM.
	 * @return {null} Method doesn't return a value.
	 */
	componentDidMount() {
		//this.interval = setInterval(this._shuffle, 2000);

		// if(this.state.rules)
		// {
		// 	setTimeout(() => {
		// 		this.setState({
		// 		  rules: this.state.rules.concat([{ id: 5, text: "See how I fade in?" }])
		// 		});
		// 	  }, 3000);
		  
		// 	  setTimeout(() => {
		// 		this.setState({
		// 		  rules: [{ id: 6, text: "Can also fade in on top" }].concat(
		// 			this.state.rules
		// 		  )
		// 		});
		// 	  }, 6000);
		// }
	},

	// _shuffle () {
	// 	this.setState({ rules: shuffle(this.state.rules) });
	// },

	/**
	 * Method called when component will successfully unmount from the ReactDOM.
	 * @return {null} Method doesn't return a value.
	 */
	componentWillUnmount() {
		clearInterval(this.interval);
    },

	/**
	 * Render the component to the ReactDOM.
	 * @return {Object} JSX Expression.
	 */
	render() {
		let result = (null);
		if(this.props.visible && this.state.rules)
		{
			result = (
                <div className="rules-dashboard2">
					 <ItemList items={this.state.rules} />
                </div>
            );
		}

		return result;		
	}
});

module.exports = RulesListing;
