import { Command } from '../_notgenerated/helpers';

export class CommandHandler {
    localChanges: { index: number, description }[];

    private stack: Command[];
    private index: number;
    
    constructor() {
        this.stack = [];
        this.index = -1;
    }

    execute(command: Command, trackChange: boolean = true) {
        command.execute();
        if (trackChange) {
            this.stack.push(command);
            this.index++;

            this.refreshLocalChanges();
        }
    }

    reverse() {
        if(this.index < 0)
            return;

        const command = this.stack[this.index];
        command.reverse();
        this.stack.pop();
        this.index--;

        this.refreshLocalChanges();
    }

    getChanges() {
        return this.stack;
    }

    cleanStack() {
        this.stack = [];
        this.index = -1;

        this.refreshLocalChanges();
    }

    private refreshLocalChanges() {
        this.localChanges = [];
        this.stack.forEach((x, i) => this.localChanges.push({ index: i, description: x.description }));
    }
}