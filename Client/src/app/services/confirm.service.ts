import { Injectable } from '@angular/core';
import { ConfirmDialogInfo } from '../_notgenerated/helpers';
import { Subject, BehaviorSubject, Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ConfirmDialogService {
    
    private dialogInfo$: Subject<ConfirmDialogInfo> = new BehaviorSubject(null);
    private dialogResult$: Subject<boolean> = new BehaviorSubject(null);

    open(message: string, cancelVisible: boolean= true, okLabel: string = 'OK', cancelLabel: string = 'Cancel'): Observable<boolean> {
        const info = {
            visible: true,
            message: message,
            okLabel: okLabel,
            cancelLabel: cancelLabel,
            cancelVisible: cancelVisible
        } as ConfirmDialogInfo;

        this.dialogInfo$.next(info);
        return this.dialogResult$.asObservable();
    }

    close() {
        const info = { visible: false } as ConfirmDialogInfo;
        this.dialogInfo$.next(info);
    }

    get confirmDialogInfo() {
        return this.dialogInfo$.asObservable();
    }

    setDialogResult(result: boolean) {
        this.close();
        this.dialogResult$.next(result);
    }
}