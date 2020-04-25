import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'create-cocktail-dialog',
    templateUrl: './create-cocktail-dialog.component.html',
    styleUrls: ['./create-cocktail-dialog.component.scss']
})
export class CreateCocktailDialog implements OnInit {

    visible: boolean = false;
    
    constructor() { }

    ngOnInit(): void {
    }

    open() {
        this.visible = true;
        console.log(this.visible)
    }

}
