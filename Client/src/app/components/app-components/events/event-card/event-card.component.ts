import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { IAppEventDto } from 'src/app/_generated/interfaces';
import { eCommandType } from 'src/app/_generated/enums';
import { ConfirmDialogService } from 'src/app/services/confirm.service';

@Component({
    selector: 'app-event-card',
    templateUrl: './event-card.component.html',
    styleUrls: ['./event-card.component.scss']
  })
export class EventCardComponent implements OnInit {
    @Input('index') index: number;
    @Input('commandType') commandType: eCommandType;
    @Input('description') description: string;

    @Output('onChangeRevert') onChangeRevert: EventEmitter<number> = new EventEmitter<number>();
    
    constructor(private confirmDialog: ConfirmDialogService) {}
    
    _eCommandType = eCommandType;

    ngOnInit() {}

    isEventTypeOf(commandType: eCommandType): boolean {
        return commandType === this.commandType;
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