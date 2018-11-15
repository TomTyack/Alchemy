
import axios from 'axios';

export class RuleDataService  {

    constructor() {
    }

    ReadRules() {
        const request = axios.get(`http://sc827/alchemy/api/rules/ruleslist/`);
        return request.then(res => {
            return res;
        });

    }
}
