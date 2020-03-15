import { Component, OnInit } from "@angular/core";
import { IAppEventDto } from "src/app/_generated/interfaces";
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: "app-event-panel",
  templateUrl: "./event-panel.component.html",
  styleUrls: ["./event-panel.component.scss"]
})
export class EventPanelComponent implements OnInit {
  dataLoaded = false;
  events: IAppEventDto[];
  
  constructor(private api: ApiService) {}

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
