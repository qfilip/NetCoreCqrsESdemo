import { Component, OnInit, OnDestroy } from '@angular/core';
import { ConfirmDialogService } from 'src/app/services/confirm.service';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { ConfirmDialogInfo } from 'src/app/_notgenerated/helpers';

@Component({
    selector: 'app-confirm-modal',
    templateUrl: './confirm-modal.component.html',
    styleUrls: ['./confirm-modal.component.scss']
})
export class ConfirmModalDialog implements OnInit {
    
    dialogInfo: ConfirmDialogInfo;

    ngOnInit() {
        this.dialogInfo = {
            visible: false,
            acceptFn: () => {},
            message: '',
            okLabel: 'Ok',
            cancelLabel: 'Cancel',
            cancelVisible: true
        } as ConfirmDialogInfo;
    }

    open(message: string, acceptFn: () => void, cancelVisible: boolean = true, okLabel: string = 'OK', cancelLabel: string = 'Cancel') {
        const info = {
            visible: true,
            acceptFn: acceptFn,
            message: message,
            okLabel: okLabel,
            cancelLabel: cancelLabel,
            cancelVisible: cancelVisible
        } as ConfirmDialogInfo;

        this.dialogInfo = info;
    }

    onConfirm() {
        this.dialogInfo.acceptFn();
        this.dialogInfo.visible = false;
    }

    onDeny() {
        this.dialogInfo.visible = false;
    }
}
