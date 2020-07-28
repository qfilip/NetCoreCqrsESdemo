import { DtoFunctions } from 'src/app/functions/dtoFunctions';
import { ICocktailDto, IIngredientDto, IRecipeExcerptDto } from 'src/app/_generated/interfaces';
import { Output, EventEmitter, OnInit } from '@angular/core';
import { IngredientController } from 'src/app/services/controllers/ingredient-controller.service';
import { PageLoaderService } from 'src/app/services/page-loader.service';

export class CocktailDialogFunctions {
    visible = false;
    errorMessages: string[]= [];
    dtoFuncs: DtoFunctions;
    
    cocktail: ICocktailDto;
    ingredients: IIngredientDto[];
    excerpts: IRecipeExcerptDto[] = [];

    @Output('onConfirmed') emitter = new EventEmitter<ICocktailDto>();

    constructor(
        protected controller: IngredientController,
        protected pageLoader: PageLoaderService
    ) {
        this.visible = false;
        this.cocktail = {} as ICocktailDto;
        this.dtoFuncs = new DtoFunctions();
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

    protected onConfirmValidate() {
        this.errorMessages = [];

        if(this.cocktail.name.length === 0) {
            this.errorMessages.push('Cocktail name cannot be empty');
        }

        if(this.cocktail.excerpts.length === 0) {
            this.errorMessages.push('Cocktail must contain at least one ingredient');
        }
    }
}