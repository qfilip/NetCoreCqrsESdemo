import { Component, OnInit, ViewChild } from '@angular/core';
import { PageLoaderService } from 'src/app/services/page-loader.service';
import { ApiService } from 'src/app/services/api.service';
import { IIngredientDto, ICommandInfo, IAppEventDto } from 'src/app/_generated/interfaces';
import { Command } from 'src/app/_notgenerated/helpers';
import { CreateIngredientDialog } from './create-ingredient-dialog/create-ingredient.dialog';
import { eCommand, eCommandType } from 'src/app/_generated/enums';
import { CommandHandler } from 'src/app/functions/commandFunctions';
import { EditIngredientDialog } from './edit-ingredient-dialog/edit-ingredient.dialog';
import { ConfirmModalDialog } from 'src/app/shared/confirm-modal/confirm-modal.component';
import { IngredientController } from 'src/app/services/controllers/ingredient-controller.service';

@Component({
    selector: 'app-ingredients',
    templateUrl: './ingredients.component.html',
    styleUrls: ['./ingredients.component.scss']
})
export class IngredientsComponent implements OnInit {

    ingredients: IIngredientDto[] = [];
    handler: CommandHandler<IIngredientDto>;

    @ViewChild('acid') createDialog: CreateIngredientDialog;
    @ViewChild('aeid') editDialog: EditIngredientDialog;
    @ViewChild('confirmModal') confirmModal: ConfirmModalDialog;

    constructor(
        private controller: IngredientController,
        private pageLoader: PageLoaderService
    ) { }

    ngOnInit() {
        this.getIngredients();
        this.handler = new CommandHandler<IIngredientDto>();
    }

    private getIngredients() {
        this.ingredients = [];
        this.pageLoader.show('Fetching ingredients');
        this.controller.getAllIngredients()
            .subscribe(
                response => {
                    this.ingredients = response;
                    this.pageLoader.hide();
                },
                error => {
                    this.pageLoader.hide();
                });
    }

    openCreateDialog() {
        this.createDialog.open();
    }

    onIngredientCreate(e: IIngredientDto) {
        const description = `Created ${e.name} ingredient`;
        const command = new Command(e, this.ingredients, eCommand.CreateIngredientCommand, eCommandType.Create, description);
        this.handler.execute(command);
    }

    openEditDialog(e: IIngredientDto) {
        this.editDialog.open(e);
    }

    onIngredientEdit(e: IIngredientDto) {
        const description = `Edited ${e.name} ingredient`;
        const command = new Command(e, this.ingredients, eCommand.EditIngredientCommand, eCommandType.Edit, description);
        this.handler.execute(command);
    }

    openRemoveDialog(ingredientId: string) {
        const ingredient = this.ingredients.find(x => x.id === ingredientId);
        const command = new Command(ingredient, this.ingredients, eCommand.RemoveIngredientCommand, eCommandType.Remove);
        this.handler.execute(command);
    }

    saveChanges() {
        const payloads = this.handler.getCommandPayload();

        if (payloads.length === 0) {
            this.confirmModal.open('No changes were made', () => {}, false);
            return;
        }

        const proceed = () => {
            this.controller.executeCommands(payloads)
                .subscribe(result => {
                    this.handler.cleanStack();
                    this.changesSavedSuccessMessage();
                    this.getIngredients();
                });
        }

        this.confirmModal.open('Pending changes are about to be saved. Proceed?', proceed);
    }

    undo() {
        this.handler.reverse();
    }

    revertToChange(changeIndex: number) {
        const lastChange = this.handler.localChanges.length - 1;
        if(lastChange === changeIndex) {
            const msg = 'Cannot revert to last change. Delete the change, or revert to an earlier one.';
            const cancelVsible = false;
            this.confirmModal.open(msg, () => {}, cancelVsible);
            
            return;
        }

        this.handler.revertToChange(changeIndex);
    }

    private changesSavedSuccessMessage() {
        const msg = 'Local changes are saved in the database, and transfered to events page';
        const cancelVsible = false;
        this.confirmModal.open(msg, () => {}, cancelVsible);
    }
}
