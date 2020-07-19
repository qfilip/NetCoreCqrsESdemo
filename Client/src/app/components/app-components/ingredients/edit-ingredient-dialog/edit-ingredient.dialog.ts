import { Component, Output, EventEmitter } from '@angular/core';
import { IIngredientDto } from 'src/app/_generated/interfaces';
import { DtoFunctions } from 'src/app/functions/dtoFunctions';

@Component({
    selector: 'app-edit-ingredient-dialog',
    templateUrl: './edit-ingredient.dialog.html',
    styleUrls: ['./edit-ingredient.dialog.scss']
})
export class EditIngredientDialog {
    @Output('onConfirmed') emitter = new EventEmitter<IIngredientDto>();
    
    constructor() {}
    
    visible = false;
    errorMessage = '';
    ingredient: IIngredientDto;
    dtoFuncs: DtoFunctions;

    onConfirm() {
        if(this.ingredient.name.length > 0) {
            this.emitter.emit(this.ingredient);
            this.visible = false;
            return;
        }

        this.errorMessage = 'Ingredient name cannot be empty';
    }

    open(ingredient: IIngredientDto) {
        this.ingredient = ingredient;
        this.visible = true;
    }

    onClose() {
        this.visible = false;
    }

}