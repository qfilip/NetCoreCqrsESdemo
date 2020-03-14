import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-event-panel",
  templateUrl: "./event-panel.component.html",
  styleUrls: ["./event-panel.component.scss"]
})
export class EventPanelComponent implements OnInit {
  events: number[];
  constructor() {}

  ngOnInit() {
      this.events = [];
      for(let i = 0; i < 100; i++) {
          this.events.push(i);
      }
  }
}
