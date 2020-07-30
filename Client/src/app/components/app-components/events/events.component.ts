import { Component, OnInit } from '@angular/core';
import { ApiService } from 'src/app/services/api.service';

@Component({
    selector: 'app-events',
    templateUrl: './events.component.html',
    styleUrls: ['./events.component.scss']
})
export class EventsComponent {

    constructor(private api: ApiService) { }


    reseed() {
        this.api.reseedDb().subscribe(r => alert('Done'));
    }
}
