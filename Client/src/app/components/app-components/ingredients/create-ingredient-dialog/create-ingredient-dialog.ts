import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-create-ingredient-dialog',
    templateUrl: './create-ingredient-dialog.html',
    styleUrls: ['./create-ingredient-dialog.scss']
})
export class CreateIngredientDialog implements OnInit {
    visible: boolean;
    
    constructor() {
        this.visible = false;
    }
    
    ngOnInit(): void {
    }

    protected onConfirm() {

    }

    protected onCLose() {
        this.visible = false;
    }

    open() {
        this.visible = true;
    }
}
