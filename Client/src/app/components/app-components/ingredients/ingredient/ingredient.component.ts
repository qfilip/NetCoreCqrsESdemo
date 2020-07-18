import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IIngredientDto } from 'src/app/_generated/interfaces';

@Component({
    selector: 'app-ingredient',
    templateUrl: './ingredient.component.html',
    styleUrls: ['./ingredient.component.scss']
})
export class IngredientComponent implements OnInit {

    @Output('onEditClicked') onEditClicked = new EventEmitter<IIngredientDto>();
    @Input('ingredient') ingredient: IIngredientDto;

    constructor() { }

    openEditDialog() {
        this.onEditClicked.emit(this.ingredient);
    }
    ngOnInit(): void {
    }

}
