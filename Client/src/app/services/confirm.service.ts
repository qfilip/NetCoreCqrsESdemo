import { Injectable } from '@angular/core';
import { ConfirmDialogInfo } from '../_notgenerated/helpers';
import { Subject, BehaviorSubject, Observable } from 'rxjs';

// Unused - Left for example
@Injectable({providedIn: 'root'})
export class ConfirmDialogService {
    
    private dialogInfo$: Subject<ConfirmDialogInfo> = new BehaviorSubject(null);
    private dialogResult$: Subject<boolean> = new BehaviorSubject(null);
    private openDialogResult$: Subject<boolean> = new BehaviorSubject(false);

    // open(message: string, accept: () => void, cancelVisible: boolean = true, okLabel: string = 'OK', cancelLabel: string = 'Cancel'): Oservable<boolean> {
    //     const info = {
    //         visible: true,
    //         message: message,
    //         okLabel: okLabel,
    //         cancelLabel: cancelLabel,
    //         cancelVisible: cancelVisible
    //     } as ConfirmDialogInfo;

    //     this.dialogInfo$.next(info);
    //     return this.openDialogResult$.asObservable();
    // }

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