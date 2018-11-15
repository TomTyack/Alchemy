
import axios from 'axios';

export class RuleDataService  {

    constructor() {
    }

    ReadRules() {
        const request = axios.get(`http://movieworld.vrtp.local/alchemy/api/rules/ruleslist/`);
        return request.then(res => {
            return res;
        });

    }
}
