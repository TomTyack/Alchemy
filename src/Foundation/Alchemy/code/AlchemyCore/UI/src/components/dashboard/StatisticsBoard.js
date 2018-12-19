// Base React modules.

import './../../scss/dashboard.scss'
import 'react-circular-progressbar/dist/styles.css';

const React = require('react');
const createReactClass = require('create-react-class');
const PropTypes = require('prop-types');
import posed, { PoseGroup } from "react-pose";
import CircularProgressbar from 'react-circular-progressbar';

const Container = posed.div({
	enter: { opacity: 1,
		beforeChildren: true,
		transition: { duration: 1000, ease: "linear" } },
	exit: { opacity: 0 }
  });

const StatisticsBoard = createReactClass({
	/**
	 * Define the default component state.
	 * @return {Object} The default `this.state` object.
	 */
	getInitialState() {
		return {
			//rules: []
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

	},

	/**
	 * Method called when component has mounted successfully to the ReactDOM.
	 * @return {null} Method doesn't return a value.
	 */
	componentDidMount() {
		this.reRenderPercentages();
	},

	reRenderPercentages(aClass){
		
	},

	/**
	 * Method called when component will successfully unmount from the ReactDOM.
	 * @return {null} Method doesn't return a value.
	 */
	componentWillUnmount() {
		
    },

	renderRulesProgressCircles()
	{

		let rulesStarted = (null);
		if (this.props.dataService && this.props.dataService.rulesStarted) {
			let percentageProgress = this.props.dataService.rulesCompleted.length / this.props.dataService.rulesStarted.length;

			//if(percentageProgress < 100)
			//{
				rulesStarted = (<div className="circ-container"><CircularProgressbar strokeWidth="10" sqSize="60" percentage={percentageProgress} text={"In Progress"} className="inprogress" /></div>);
			//}
		}

		return (
			<div className="stats">
				{rulesStarted}
				<div className="circ-container"><CircularProgressbar strokeWidth="10" sqSize="60" percentage={100} text={"Pass"} className="success" /></div>
				<div className="circ-container"><CircularProgressbar strokeWidth="10" sqSize="60" percentage={100} text={"Fail"} className="failure" /></div>
			</div>			
		);
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
				<PoseGroup animateOnMount={true} className="dashboard-overview overlay-top">
					<Container key={"dashKey"}>
					
							{this.renderRulesProgressCircles()}
						
					</Container>
				</PoseGroup>
            );
		}

		return result;		
	}
});

module.exports = StatisticsBoard;
