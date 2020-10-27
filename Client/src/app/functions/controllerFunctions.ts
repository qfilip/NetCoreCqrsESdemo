import { HttpRequest } from '@angular/common/http';

export class ControllerFunctions {
    static generateFormDataRequest(url: string, key: string, values: string[], withCredentials = false) {
        let formData = new FormData();
        
        values.forEach(x => formData.append(key, x));

        const request = new HttpRequest (
            'POST',
            url,
            formData,
            { withCredentials: withCredentials }
        );


        return request;
    }
}