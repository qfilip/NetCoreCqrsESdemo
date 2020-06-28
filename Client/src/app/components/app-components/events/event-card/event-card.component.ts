import { Component, Input, OnInit } from '@angular/core';
import { IAppEventDto } from 'src/app/_generated/interfaces';
import { eEventType } from 'src/app/_generated/enums';

@Component({
    selector: 'app-event-card',
    templateUrl: './event-card.component.html',
    styleUrls: ['./event-card.component.scss']
  })
export class EventCardComponent implements OnInit {
    _eEventType = eEventType;

    @Input('event') appEvent: IAppEventDto = {} as IAppEventDto;

    ngOnInit() {}

    isEventTypeOf(eventType: eEventType): boolean {
        return eventType === this.appEvent.eventType;
    }
}