import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { IAppEventDto } from 'src/app/_generated/interfaces';
import { eEventType } from 'src/app/_generated/enums';
import { ConfirmDialogService } from 'src/app/services/confirm.service';

@Component({
    selector: 'app-event-card',
    templateUrl: './event-card.component.html',
    styleUrls: ['./event-card.component.scss']
  })
export class EventCardComponent implements OnInit {
    @Input('index') index: number;
    @Input('event') appEvent: IAppEventDto = {} as IAppEventDto;
    @Input('description') description: string;

    @Output('onChangeRevert') onChangeRevert: EventEmitter<number> = new EventEmitter<number>();
    
    constructor(private confirmDialog: ConfirmDialogService) {}
    
    _eEventType = eEventType;

    ngOnInit() {}

    isEventTypeOf(eventType: eEventType): boolean {
        return eventType === this.appEvent.eventType;
    }

    chooseAction() {
        const message = `Event description: ${this.description}. Revert to this?`;
        this.confirmDialog.open(message, true, 'Revert')
            .subscribe(result => {
                if(!!result) {
                    this.onChangeRevert.emit(this.index);
                }
            });
    }
}