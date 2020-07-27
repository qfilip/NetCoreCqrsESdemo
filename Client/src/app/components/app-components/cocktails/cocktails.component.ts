import { Component, OnInit, ViewChild } from '@angular/core';
import { ICocktailDto } from 'src/app/_generated/interfaces';
import { CommandHandler } from 'src/app/functions/commandFunctions';
import { ApiService } from 'src/app/services/api.service';
import { PageLoaderService } from 'src/app/services/page-loader.service';
import { eControllerType } from 'src/app/_notgenerated/enums';
import { eCommand, eCommandType } from 'src/app/_generated/enums';
import { Command } from 'src/app/_notgenerated/helpers';
import { CreateCocktailDialog } from './create-cocktail-dialog/create-cocktail.dialog';
import { ConfirmModalDialog } from 'src/app/shared/confirm-modal/confirm-modal.component';
import { EditCocktailDialog } from './edit-cocktail-dialog/edit-cocktail.dialog';
import { CocktailController } from 'src/app/services/controllers/cocktail-controller.service';

@Component({
  selector: 'app-cocktails',
  templateUrl: './cocktails.component.html',
  styleUrls: ['./cocktails.component.scss']
})
export class CocktailsComponent implements OnInit {

    cocktails: ICocktailDto[] = [];
    handler: CommandHandler<ICocktailDto>;

    @ViewChild('accd') createDialog: CreateCocktailDialog;
    @ViewChild('aecd') editDialog: EditCocktailDialog;
    @ViewChild('confirmModal') confirmModal: ConfirmModalDialog;

    constructor(
        private controller: CocktailController,
        private pageLoader: PageLoaderService
    ) { }

    ngOnInit() {
        this.getCocktails();
        this.handler = new CommandHandler<ICocktailDto>();
    }

    private getCocktails() {
        this.cocktails = [];
        this.pageLoader.show(`Fetchin' cocktails...`);
        this.controller.getAllCocktails()
            .subscribe(
                response => {
                    this.cocktails = response;
                    this.pageLoader.hide();
                },
                error => {
                    this.pageLoader.hide();
                });
    }

    openCreateDialog() {
        this.createDialog.open();
    }

    onCocktailCreate(e: ICocktailDto) {
        const description = `Created ${e.name} cocktail`;
        const command = new Command(e, this.cocktails, eCommand.CreateCocktailCommand, eCommandType.Create, description);
        this.handler.execute(command);
    }

    openEditDialog(e: ICocktailDto) {
        this.editDialog.open(e);
    }

    onCocktailEdit(e: ICocktailDto) {
        const description = `Edited ${e.name} cocktail`;
        const command = new Command(e, this.cocktails, eCommand.EditCocktailCommand, eCommandType.Edit, description);
        this.handler.execute(command);
    }

    openRemoveDialog(cocktailId: string) {
        const cocktail = this.cocktails.find(x => x.id === cocktailId);
        const command = new Command(cocktail, this.cocktails, eCommand.RemoveCocktailCommand, eCommandType.Remove);
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
                    this.getCocktails();
                });
        }

        this.confirmModal.open('Pending changes are about to be saved. Proceed?', proceed);
    }

    undo() {
        this.handler.reverse();
    }

    revertToChange(changeIndex: number) {
        this.handler.revertToChange(changeIndex);
    }

    private changesSavedSuccessMessage() {
        const msg = 'Local changes are saved in the database, and transfered to events page';
        const cancelVsible = false;
        this.confirmModal.open(msg, () => {}, cancelVsible);
    }

    clogData() {
        console.table(this.cocktails);
    }

    reseed() {
        // this.controller.reseedDb().subscribe(r => console.log(r));
    }
    
    testApiService() {
        // this.controller.test();
    }

}
