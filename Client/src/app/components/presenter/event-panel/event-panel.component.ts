import { Component, OnInit, Input } from "@angular/core";
import { IAppEventDto } from "src/app/_generated/interfaces";
import { ApiService } from 'src/app/services/api.service';
import { eSaveChangeType } from 'src/app/_generated/enums';

@Component({
    selector: "app-event-panel",
    templateUrl: "./event-panel.component.html",
    styleUrls: ["./event-panel.component.scss"]
})
export class EventPanelComponent implements OnInit {
    dataLoaded = false;
    events: IAppEventDto[];
    localChangesDisabled = true;

    @Input() localChangesEnabled: boolean = false;

    constructor(private api: ApiService) { }

    ngOnInit() {
        this.getEvents();
    }

    getEvents() {
        this.api.getAllEvents()
            .subscribe(result => {
                this.events = result;
                this.dataLoaded = true;
            })
    }
}
