import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { IIngredientDto } from 'src/app/_generated/interfaces';
import { DtoFunctions } from 'src/app/functions/dtoFunctions';

@Component({
    selector: 'app-create-ingredient-dialog',
    templateUrl: './create-ingredient.dialog.html',
    styleUrls: ['./create-ingredient.dialog.scss']
})
export class CreateIngredientDialog {
    visible = false;
    errorMessage = '';
    ingredient: IIngredientDto;
    dtoFuncs: DtoFunctions;

    @Output('onConfirmed') emitter = new EventEmitter<IIngredientDto>();
    
    constructor() {
        this.dtoFuncs = new DtoFunctions();
        this.reset();
    }

    onConfirm() {
        if(this.ingredient.name.length > 0) {
            this.ingredient.id = this.dtoFuncs.generateId();
            this.emitter.emit(this.ingredient);
            this.reset();
            this.visible = false;
            return;
        }

        this.errorMessage = 'Ingredient name cannot be empty';
    }

    onClose() {
        this.visible = false;
    }

    open() {
        this.reset();
        this.visible = true;
    }

    private reset() {
        this.ingredient = { name: '', strength: 0 } as IIngredientDto;
        this.errorMessage = '';
    }
}
