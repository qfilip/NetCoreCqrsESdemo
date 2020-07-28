import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { ICocktailDto } from 'src/app/_generated/interfaces';
import { CocktailDialogFunctions } from '../cocktail-dialog.functions';

@Component({
    selector: 'app-create-cocktail-dialog',
    templateUrl: './create-cocktail.dialog.html',
    styleUrls: ['./create-cocktail.dialog.scss']
  })
export class CreateCocktailDialog extends CocktailDialogFunctions {

    onConfirm() {
        this.errorMessages = [];
        if(this.cocktail.name.length > 0 && this.cocktail.excerpts.length > 0) {
            this.cocktail.id = this.dtoFuncs.generateId();
            this.emitter.emit(this.cocktail);
            this.reset();
            this.visible = false;
            
            return;
        }

        this.onConfirmValidate();
    }

    onClose() {
        this.visible = false;
    }

    open() {
        this.reset();
        this.getRequiredData();
    }

    private getRequiredData() {
        this.pageLoader.show('Getting required data');
        this.controller.getAllIngredients()
            .subscribe(
                result => {
                    this.ingredients = result;
                    this.visible = true;
                    this.pageLoader.hide();
                },
                error => {
                    this.pageLoader.hide();
                }
            );
    }

    private reset() {
        const id = this.dtoFuncs.generateId();
        this.cocktail = { id: id, name: '', excerpts: [] } as ICocktailDto;
        this.ingredients = [];
        this.excerpts = [];
        this.errorMessages = [];
    }
}
