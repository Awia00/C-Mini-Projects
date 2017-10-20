import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { LinkModel } from './LinkModel';

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})

export class HomeComponent {
    private generatedLink = "";
    private http: Http;
    private baseUrl: string;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.http = http;
        this.baseUrl = baseUrl;
    }

    generateLink(name: string, url: string): void {
        this.http.post(this.baseUrl + 'api/Links', new LinkModel(name, url))
            .subscribe(result => {
                this.generatedLink = (result.json() as LinkModel).localUri;
            }, error => console.error(error));
    };
}