// Base React modules.

import './../../scss/dashboard.scss'
import 'react-circular-progressbar/dist/styles.css';

const React = require('react');
const createReactClass = require('create-react-class');
const PropTypes = require('prop-types');
import posed, { PoseGroup } from "react-pose";
import CircularProgressbar from 'react-circular-progressbar';


const Rule = posed.div({
	enter: { opacity: 1 },
	exit: { opacity: 0 }
  })

// const ItemList = ({ items }) => (
// 	<ul className="rules-list">
// 	  <PoseGroup>
// 		{
// 		items.map(function(item, i){
// 			let successClass = item.Success ? "success" : "failure";
// 			let itemClassName = "rule-block " + successClass;
// 			return (<Rule className={itemClassName} key={item.Id}>
// 						<div className="name">{item.Name}</div>
// 						<div className="progress"><CircularProgressBar strokeWidth="10"
//               				sqSize="200"
//               				percentage="100"  /></div>
// 					</Rule>
// 				);
// 		})}
// 	  </PoseGroup>
// 	</ul>
// );

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

			if(this.props.data.pendingArray)
			pendingArray = pendingArray.concat(this.props.data.pendingArray)
				//pendingArray.push(this.props.data.pendingArray); // Add existing items already in the state

			this.props.data.pendingArray = pendingArray.slice(0);

			this.props.dataService.dashboardPending = [];
			this.setState({
				rules: this.props.data.pendingArray
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
		this.reRenderPercentages();
	},

	reRenderPercentages(aClass){
		let thisClass = this;
		if(aClass)
			thisClass = aClass;
		setTimeout(() => {

			if(thisClass.state.rules && thisClass.state.rules.length > 0)
			{
				thisClass.state.rules.map(function(item, i){
					if(item.Success && item.percentage) item.percentage = item.percentage+10;
					if(item.percentage > 110)
					{
						thisClass.state.rules.pop(item);
					}
				});
				thisClass.setState({});
				
			}		

			thisClass.reRenderPercentages(thisClass);
		}, 3000);
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

	renderList(){
		var items = this.state.rules;
		if(items && items.length > 0)
		{
			//const percentage = 66;

			let listing = items.map(function(item, i){
				let successClass = item.Success ? "success" : "failure";
				let itemClassName = "rule-block " + successClass;
				let progress = "";
				if(item.Success)
				{
					if(!item.percentage) item.percentage = 10;
					progress = (<div className="progressbar"><CircularProgressbar strokeWidth="10" sqSize="200" percentage={item.percentage} className="success" /></div>);
				}
				return (<Rule className={itemClassName} key={item.Id}>
							<div className="name">{item.Name}</div>
							{progress}							
						</Rule>
					);
			});

			return (<ul className="rules-list">
				<PoseGroup>
					{listing}
				</PoseGroup>
			</ul>);
		}
		
		return (null);
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
                <div className="rules-dashboard overlay">
					 {this.renderList()}
                </div>
            );
		}

		return result;		
	}
});

module.exports = RulesListing;
