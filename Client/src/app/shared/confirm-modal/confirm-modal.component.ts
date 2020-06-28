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
export class ConfirmModalComponent implements OnInit, OnDestroy {
    constructor(private confirService: ConfirmDialogService) {}
    
    dialogInfo: ConfirmDialogInfo;
    unsubscribe: Subject<any> = new Subject();

    ngOnInit(): void {
        this.confirService.confirmDialogInfo
            .pipe(takeUntil(this.unsubscribe))
            .subscribe(dialogInfo => {
                if(!!dialogInfo) {
                    this.dialogInfo = dialogInfo;
                }
            });
    }

    ngOnDestroy(): void {
        this.unsubscribe.next();
        this.unsubscribe.complete();
    }
}
