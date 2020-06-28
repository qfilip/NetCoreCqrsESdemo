import { Injectable } from '@angular/core';
import { ConfirmDialogInfo } from '../_notgenerated/helpers';
import { Subject, BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ConfirmDialogService {
    
    private dialogInfo$: Subject<ConfirmDialogInfo> = new BehaviorSubject(null);

    open(message: string, okLabel: string = 'OK', cancelLabel: string = 'Cancel', cancelVisible: boolean= true) {
        const info = {
            visible: true,
            message: message,
            okLabel: okLabel,
            cancelLabel: cancelLabel,
            cancelVisible: cancelVisible
        } as ConfirmDialogInfo;

        this.dialogInfo$.next(info);
    }

    close() {
        const info = { visible: false } as ConfirmDialogInfo;
        this.dialogInfo$.next(info);
    }

    get confirmDialogInfo() {
        return this.dialogInfo$.asObservable();
    }
}