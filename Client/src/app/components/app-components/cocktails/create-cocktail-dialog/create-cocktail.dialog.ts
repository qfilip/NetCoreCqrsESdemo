import { Component, OnInit, EventEmitter, Output, Input } from '@angular/core';
import { ICocktailDto, IIngredientDto, IRecipeExcerptDto } from 'src/app/_generated/interfaces';
import { DtoFunctions } from 'src/app/functions/dtoFunctions';
import { ApiService } from 'src/app/services/api.service';
import { IngredientController } from 'src/app/services/controllers/ingredient-controller.service';
import { PageLoaderService } from 'src/app/services/page-loader.service';

@Component({
  selector: 'app-create-cocktail-dialog',
  templateUrl: './create-cocktail.dialog.html',
  styleUrls: ['./create-cocktail.dialog.scss']
})
export class CreateCocktailDialog {

    visible = false;
    errorMessages: string[]= [];
    dtoFuncs: DtoFunctions;
    
    cocktail: ICocktailDto;
    ingredients: IIngredientDto[];
    excerpts: IRecipeExcerptDto[] = [];

    @Output('onConfirmed') emitter = new EventEmitter<ICocktailDto>();
    
    constructor(
        private controller: IngredientController,
        private pageLoader: PageLoaderService
    ) {
        this.dtoFuncs = new DtoFunctions();
        this.reset();
    }

    onConfirm() {
        this.errorMessages = [];
        if(this.cocktail.name.length > 0 && this.cocktail.excerpts.length > 0) {
            this.cocktail.id = this.dtoFuncs.generateId();
            this.emitter.emit(this.cocktail);
            this.reset();
            this.visible = false;
            return;
        }

        if(this.cocktail.name.length === 0) {
            this.errorMessages.push('Cocktail name cannot be empty');
        }

        if(this.cocktail.excerpts.length === 0) {
            this.errorMessages.push('Cocktail must contain at least one ingredient');
        }
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

    createExcerpt() {
        if(this.ingredients.length === 0) {
            this.errorMessages = ['All available ingredients are used'];
            return;
        }

        const id = this.dtoFuncs.generateId();
        const ingredient = this.ingredients[0];
        const excerpt = {
            id: id,
            amount: 0,
            cocktailId: this.cocktail.id,
            ingredientId: ingredient.id,
        } as IRecipeExcerptDto;

        this.excerpts.push(excerpt)
        this.cocktail.excerpts = this.excerpts;
    }
    
    removeExcerpt(excerpt: IRecipeExcerptDto) {
        this.excerpts = this.excerpts.filter(x => x.id !== excerpt.id);
    }

    changeIngredient(excerpt: IRecipeExcerptDto, e: any) {
        const ingredientId = e.target.value;
        excerpt.ingredientId = ingredientId;
    }

    changeIngredientAmount(excerpt: IRecipeExcerptDto, e: any) {
        const amount = e.target.value;
        excerpt.amount = amount;
    }

    private reset() {
        const id = this.dtoFuncs.generateId();
        this.cocktail = { id: id, name: '', excerpts: [] } as ICocktailDto;
        this.ingredients = [];
        this.excerpts = [];
        this.errorMessages = [];
    }
}
