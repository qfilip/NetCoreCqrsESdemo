import { eCommand, eEventType } from '../_generated/enums';
import { IBaseDto, IAppEventDto } from '../_generated/interfaces';

export class PageLoaderInfo {
    loading: boolean;
    message: string;
}

export class ConfirmDialogInfo {
    visible: boolean;
    message: string;
    okLabel: string;
    cancelLabel: string;
    cancelVisible: boolean;
}

export class Command {
    private array: IBaseDto[];
    
    constructor(parameter: IBaseDto, array: IBaseDto[], type: eCommand, event: IAppEventDto, description: string = 'no description') {
        this.parameter = parameter;
        this.array = array;
        this.commandType = type;
        this.event = event;
        this.description = description;
        
        this.execute = () => this.array.push(parameter);
        this.reverse = () => {
            const index = this.array.findIndex(x => x.id === this.parameter.id);
            this.array.splice(index, 1);
        }
    }

    parameter: IBaseDto;
    commandType: eCommand;
    event: IAppEventDto;
    description: string;
    
    execute: () => any;
    reverse: () => any;
}