import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { ICocktailDto } from 'src/app/_generated/interfaces';
import { CocktailDialogFunctions } from '../cocktail-dialog.functions';

@Component({
  selector: 'app-edit-cocktail-dialog',
  templateUrl: './edit-cocktail.dialog.html',
  styleUrls: ['./edit-cocktail.dialog.scss']
})
export class EditCocktailDialog extends CocktailDialogFunctions {
    
    onConfirm() {
        if(this.cocktail.name.length > 0 && this.cocktail.excerpts.length > 0) {
            this.emitter.emit(this.cocktail);
            this.visible = false;
            
            return;
        }

        this.onConfirmValidate();
    }

    open(cocktail: ICocktailDto) {
        this.cocktail = cocktail;
        this.getRequiredData();
    }

    onClose() {
        this.visible = false;
    }

    private getRequiredData() {
        this.pageLoader.show('Getting required data');
        const ingredientIds = this.cocktail.excerpts.map(x => x.ingredientId);
        this.controller.getIngredientsByIds(ingredientIds)
            .subscribe(
                result => {
                    this.excerpts = [...this.cocktail.excerpts];
                    this.ingredients = [...result];
                    console.log(this.ingredients)
                    this.pageLoader.hide();
                    this.visible = true;
                },
                error => {
                    this.pageLoader.hide();
                }
            );
    }

    // private reset() {
    //     const id = this.dtoFuncs.generateId();
    //     this.cocktail = { id: id, name: '', excerpts: [] } as ICocktailDto;
    //     this.ingredients = [];
    //     this.excerpts = [];
    //     this.errorMessages = [];
    // }
}
