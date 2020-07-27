import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { ICocktailDto } from 'src/app/_generated/interfaces';
import { DtoFunctions } from 'src/app/functions/dtoFunctions';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-create-cocktail-dialog',
  templateUrl: './create-cocktail.dialog.html',
  styleUrls: ['./create-cocktail.dialog.scss']
})
export class CreateCocktailDialog {

    visible = false;
    errorMessage = '';
    cocktail: ICocktailDto;
    dtoFuncs: DtoFunctions;

    @Output('onConfirmed') emitter = new EventEmitter<ICocktailDto>();
    @Output('onDataLoaded') onDataLoaded = new EventEmitter<boolean>();
    
    constructor(private controller: ApiService) {
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
        this.visible = true;
        this.onDataLoaded.emit(true);
    }

    private getRequiredData() {
        // this.controller.
    }

    private reset() {
        this.cocktail = { name: '', excerpts: [] } as ICocktailDto;
        this.errorMessage = '';
    }

}
