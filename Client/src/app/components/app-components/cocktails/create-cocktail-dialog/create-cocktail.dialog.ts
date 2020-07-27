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
    errorMessage = '';
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
        if(this.cocktail.name.length > 0) {
            this.cocktail.id = this.dtoFuncs.generateId();
            this.emitter.emit(this.cocktail);
            this.reset();
            this.visible = false;
            return;
        }

        this.errorMessage = 'Cocktail name cannot be empty';
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
                    this.mapData();
                    this.pageLoader.hide();
                },
                error => {
                    this.pageLoader.hide();
                }
            );
    }

    private mapData() {

        this.visible = true;
    }

    private reset() {
        this.cocktail = { name: '', excerpts: [] } as ICocktailDto;
        this.errorMessage = '';
    }

}
