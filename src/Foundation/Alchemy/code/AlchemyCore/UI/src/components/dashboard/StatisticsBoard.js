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
	exit: { opacity: 0, transition: { duration: 3000, ease: "linear" } }
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
			data: {
				showProgress: true,
				progressPercentage: 0,
				passProgressPercentage: 0,
				failedProgressPercentage: 0
			},
			dataService: {},
            visible: false
		};
	},

	componentDidUpdate(prevProps) {

		if(this.props.visible)
		{
			this.reRenderProgressPercentages();
			this.reRenderPassPercentages();
			this.reRenderFailPercentages();
		}	
	},

	/**
	 * Method called when component has mounted successfully to the ReactDOM.
	 * @return {null} Method doesn't return a value.
	 */
	componentDidMount() {
		
	},

	reRenderProgressPercentages(aClass){
		let thisClass = this;
		if(aClass) thisClass = aClass;
		setTimeout(() => {
			if(thisClass.props.data.progressPercentage < 100)
			{
				let actualProgress = thisClass.getProgressPercentage(thisClass);

				thisClass.props.data.progressPercentage = thisClass.props.data.progressPercentage + 5;
				if(thisClass.props.data.progressPercentage > actualProgress)
				thisClass.props.data.progressPercentage = actualProgress;

				thisClass.setState({});
				//thisClass.reRenderProgressPercentages(thisClass);
			}else if(thisClass.props.data.showProgress){
				thisClass.props.data.showProgress = false;
				thisClass.setState({});
			}			
		}, 3000);
	},

	reRenderPassPercentages(aClass){
		let thisClass = this;
		if(aClass) thisClass = aClass;
		setTimeout(() => {
			if(thisClass.props.data.passProgressPercentage < 100)
			{
				let actualProgress = thisClass.getPassPercentage(thisClass);

				thisClass.props.data.passProgressPercentage = thisClass.props.data.passProgressPercentage + 5;
				if(thisClass.props.data.passProgressPercentage > actualProgress)
				thisClass.props.data.passProgressPercentage = actualProgress;

				thisClass.setState({});
				//thisClass.reRenderPassPercentages(thisClass);
			}		
		}, 3000);
	},

	reRenderFailPercentages(aClass){
		let thisClass = this;
		if(aClass) thisClass = aClass;
		setTimeout(() => {
			if(thisClass.props.data.failedProgressPercentage < 100)
			{
				let actualProgress = thisClass.getFailePercentage(thisClass);

				thisClass.props.data.failedProgressPercentage = thisClass.props.data.failedProgressPercentage + 5;
				if(thisClass.props.data.failedProgressPercentage > actualProgress)
				thisClass.props.data.failedProgressPercentage = thisClass.getFailePercentage(thisClass);

				thisClass.setState({});
				//thisClass.reRenderFailPercentages(thisClass);
			}		
		}, 3000);
	},

	/**
	 * Method called when component will successfully unmount from the ReactDOM.
	 * @return {null} Method doesn't return a value.
	 */
	componentWillUnmount() {

    },

	getProgressPercentage(thisClass)
	{
		if(thisClass.props.dataService.rulesStarted.length == 0)
			return 100;

		if(thisClass.props.dataService && thisClass.props.dataService.rulesCompleted)
			return thisClass.props.dataService.rulesCompleted.length / thisClass.props.dataService.rulesStarted.length;
		else
			return 0;
	},

	
	getPassPercentage(thisClass)
	{
		if(thisClass.props.dataService && thisClass.props.dataService.rulesSucceeded)
		{
			if(thisClass.props.dataService.rulesSucceeded.length == 0)
				return 0;

			return (thisClass.props.dataService.rulesCompleted.length / thisClass.props.dataService.rulesSucceeded.length) * 100;
		}
		else
			return 0;
	},

	getFailePercentage(thisClass)
	{
		if(thisClass.props.dataService && thisClass.props.dataService.rulesFailed)
		{
			if(thisClass.props.dataService.rulesFailed.length == 0)
				return 0;

			return (thisClass.props.dataService.rulesCompleted.length / thisClass.props.dataService.rulesFailed.length) * 100;
		}
		else
			return 0;
	},

	renderRulesProgressCircles()
	{

		let rulesStarted = (null);
		let rulesPass = (null);
		let rulesFailed = (null);
		if (this.props.dataService && this.props.dataService.rulesStarted) 
		{
			if(this.props.data.showProgress)
			{
				rulesStarted = (<div className="circ-container"><CircularProgressbar strokeWidth="10" sqSize="60" percentage={this.props.data.progressPercentage} text={"In Progress"} className="inprogress" /></div>);
			}
		}

		if (this.props.dataService && this.props.dataService.rulesFailed) 
		{
			rulesPass = (<div className="circ-container"><CircularProgressbar strokeWidth="10" sqSize="60" percentage={this.props.data.passProgressPercentage} text={"Pass"} className="success" /></div>);
		}

		if (this.props.dataService && this.props.dataService.rulesSucceeded) 
		{
			rulesFailed = (<div className="circ-container"><CircularProgressbar strokeWidth="10" sqSize="60" percentage={this.props.data.failedProgressPercentage} text={"Fail"} className="failure" /></div>);
		}

		return (
			<div className="stats">
				{rulesStarted}
				{rulesPass}
				{rulesFailed}
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
