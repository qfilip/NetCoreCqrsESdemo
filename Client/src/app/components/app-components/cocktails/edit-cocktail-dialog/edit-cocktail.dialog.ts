import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { ICocktailDto, IIngredientDto, IRecipeExcerptDto } from 'src/app/_generated/interfaces';
import { DtoFunctions } from 'src/app/functions/dtoFunctions';
import { IngredientController } from 'src/app/services/controllers/ingredient-controller.service';
import { PageLoaderService } from 'src/app/services/page-loader.service';

@Component({
  selector: 'app-edit-cocktail-dialog',
  templateUrl: './edit-cocktail.dialog.html',
  styleUrls: ['./edit-cocktail.dialog.scss']
})
export class EditCocktailDialog {
    visible = false;
    errorMessages = [];
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
        if(this.cocktail.name.length > 0 && this.cocktail.excerpts.length > 0) {
            this.emitter.emit(this.cocktail);
            this.visible = false;
            return;
        }

        if(this.cocktail.name.length === 0) {
            this.errorMessages.push('Ingredient name cannot be empty');
        }

        if(this.cocktail.excerpts.length === 0) {
            this.errorMessages.push('Cocktail must contain at least one ingredient');
        }
    }

    open(cocktail: ICocktailDto) {
        this.cocktail = cocktail;
        this.reset();
        this.getRequiredData();
    }

    onClose() {
        this.visible = false;
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

    private getRequiredData() {
        this.pageLoader.show('Getting required data');
        this.controller.getAllIngredients()
            .subscribe(
                result => {
                    this.excerpts = this.cocktail.excerpts;
                    this.ingredients = result;
                    this.pageLoader.hide();
                    this.visible = true;
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
