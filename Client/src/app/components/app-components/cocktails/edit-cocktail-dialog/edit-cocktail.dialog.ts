import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { ICocktailDto } from 'src/app/_generated/interfaces';
import { DtoFunctions } from 'src/app/functions/dtoFunctions';

@Component({
  selector: 'app-edit-cocktail-dialog',
  templateUrl: './edit-cocktail.dialog.html',
  styleUrls: ['./edit-cocktail.dialog.scss']
})
export class EditCocktailDialog {
    @Output('onConfirmed') emitter = new EventEmitter<ICocktailDto>();
    
    visible = false;
    errorMessage = '';
    cocktail: ICocktailDto;
    dtoFuncs: DtoFunctions;

    onConfirm() {
        if(this.cocktail.name.length > 0) {
            this.emitter.emit(this.cocktail);
            this.visible = false;
            return;
        }

        this.errorMessage = 'Ingredient name cannot be empty';
    }

    open(cocktail: ICocktailDto) {
        this.cocktail = cocktail;
        this.visible = true;
    }

    onClose() {
        this.visible = false;
    }

}
