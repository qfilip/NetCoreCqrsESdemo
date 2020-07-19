import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { IIngredientDto } from 'src/app/_generated/interfaces';

@Component({
    selector: 'app-ingredient',
    templateUrl: './ingredient.component.html',
    styleUrls: ['./ingredient.component.scss']
})
export class IngredientComponent implements OnInit {

    @Output('onEditClicked') onEditClicked = new EventEmitter<IIngredientDto>();
    @Output('onRemoveClicked') onRemoveClicked = new EventEmitter<string>();
    
    @Input('ingredient') ingredient: IIngredientDto;

    constructor() { }
    ngOnInit() {}

    openEditDialog() {
        this.onEditClicked.emit(this.ingredient);
    }

    openRemoveDialog() {
        this.onRemoveClicked.emit(this.ingredient.id);
    }
}
