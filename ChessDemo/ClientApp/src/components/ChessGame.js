import React, { Component } from 'react';
import King from './img/king.png';
import Rook from './img/rook.png';
import Bishop from './img/bishop.png';

export class ChessGame extends Component {
    static displayName = ChessGame.name;

    constructor(props) {
        super(props);
        this.state = {
            currentCount: 0,
            figureList: [],
            selectedFigure: "",
            selectedField: "",
            fieldsToMove: [],
            figurePosition: "",
            initialPosition: "",
            initialState: true
        };
    }

    render() {        
        return (
            <div>

                <div id="select-menu">

                    <p>Select figure:</p>
                    <p>
                        <select id="figure-select" onChange={ev => this.selectChange(ev.target.value)}>
                            {
                                this.state.figureList.map(fig =>
                                    <option key={fig} value={fig}>{fig}</option>
                                )}
                        </select>
                    </p>
                </div>
                <div id="chess-board">
                    <table>
                        <tbody>
                            {this.createTableContent()}

                        </tbody>

                    </table>
                </div>
            </div>
        );
    }
 
    getFigureImg() {
        if (this.state.selectedFigure.length < 1) return;
        switch (this.state.selectedFigure) {

            case "King":
                return <img src={King} width="90" height="90" />
            case "Bishop":
                return <img src={Bishop} width="90" height="90" />
            case "Rook":
                return <img src={Rook} width="90" height="90" />
            default:
                return <p>No selection</p>
        }
    }

    async fieldOnClick(key) {        

        if (this.state.initialState) {            
            this.setState({ initialPosition: key });
            return;
        }

        if (this.state.selectedField === this.state.figurePosition) {
            if (!(key === this.state.figurePosition))
            {
                this.moveFigure(key, this.state.figurePosition, this.state.selectedFigure);
            }

        } else {
            this.setState({ selectedField: key });
        };

    }


    createTableContent() {

        const rows = [0, 1, 2, 3, 4, 5, 6, 7];
        const columns = [0, 1, 2, 3, 4, 5, 6, 7];

        return rows.map(r =>
            <tr key={r}>
                {
                    columns.map(c => <td key={r + "," + c} className={this.getClass(r, c)}
                        onClick={ev => this.fieldOnClick((r + "," + c))}>
                        {
                            this.placeFigure(r + "," + c)
                        }
                    </td>)
                }
            </tr>
        );
    }
    async selectChange(val) {
        var figPos = this.state.figurePosition;
        var initPos = this.state.initialPosition;
        var isInitial = this.state.initialState;
        if ((figPos < 1) && (initPos < 1)) {
            alert('Select initial field first');
            var select = document.getElementsByTagName("SELECT")[0];
            select.selectedIndex = 0;
            return;
        }

        if (isInitial) {
            var dataStr = await this.getFieldsToMove(initPos, val);
            var fields = JSON.parse(dataStr);
            this.setState({ selectedFigure: val, figurePosition: initPos, fieldsToMove: fields, initialState: false });
        } else if (val.length > 0) {
            var dataStr = await this.getFieldsToMove(figPos, val);
            var fields = JSON.parse(dataStr);
            this.setState({ selectedFigure: val, fieldsToMove: fields, selectedField: "" });
        } else this.setState({ selectedFigure: val, fieldsToMove: [], selectedField: "" });
        
    }
    getClass(row, column) {

        var fieldType = "regular";
        const fieldKey = (row + "," + column);
        if (this.state.initialState) {
            if (fieldKey === this.state.initialPosition)
            {
                fieldType = "initial";
            }
            
        } else if (this.state.figurePosition === this.state.selectedField) {
            for (var i = 0; i < this.state.fieldsToMove.length; i++) {

                var val = this.state.fieldsToMove[i];
                if (val === fieldKey) {
                    fieldType = "available";
                    break;
                }
            }
        } 
        switch (fieldType) {
            case "available":
                return ((column + row) % 2 === 1) ? "favailable-dark" : "favailable-white";                

            case "initial":
                return ((column + row) % 2 === 1) ? "finitial-dark" : "finitial-white";                

            default:
                return ((column + row) % 2 === 1) ? "fdark" : "fwhite";

        }
    }

    placeFigure(key) {
        if (key === this.state.figurePosition) {
            return this.getFigureImg();
        }

    }

    componentDidMount() {
        this.loadData();
    }

    async loadData() {
        let dataStr = await this.getFigures();
        let figures = JSON.parse(dataStr);
        this.setState({ figureList: figures });
    }
    getFigures() {
        return new Promise((resolve, reject) => {

            var req = new XMLHttpRequest();
            req.open('GET', 'api/figure', false);
            req.send(null);

            if (req.status === 200) {

                resolve(req.response);
            }
            else {
                reject('Error. Status code: ' + req.status);
            }
        });
    }
    getFieldsToMove(key, figure) {
        return new Promise((resolve, reject) => {
            var posX = key.split(",")[0];
            var posY = key.split(",")[1];
            var _url = "/api/game?figure=" + figure;
            _url += "&posx=" + posX;
            _url += "&posy=" + posY;

            //"/api/game?figure=King&posx=0&posy=0"
            var req = new XMLHttpRequest();
            req.open('GET', _url, false);
            req.send(null);

            if (req.status === 200) {

                resolve(req.response);
            }
            else {
                reject('Error. Status code: ' + req.status);
            }
        });
    }
    
    async moveFigure(nextKey, figKey, figure)
    {
        if (figure.length < 1) return;
        var flag = false;
        var dataStr = await this.checkMove(nextKey, figKey, figure);
        flag = JSON.parse(dataStr);
 
        if (flag) {
            var dataStr = await this.getFieldsToMove(nextKey, figure);
            console.log(dataStr);
            var fields = JSON.parse(dataStr);
            this.setState({ figurePosition: nextKey, selectedField: "", fieldsToMove: fields });

        } else {
            alert('This figure can not move there');
        }
    }
    async checkMove(nextKey, figKey, figure)
    {
        return new Promise((resolve, reject) => {
            var posX = figKey.split(",")[0];
            var posY = figKey.split(",")[1];
            var nextX = nextKey.split(",")[0];
            var nextY = nextKey.split(",")[1];
            var param = posX + "|" + posY + "|" + nextX + "|" + nextY + "|" + figure;
            var _url = "/api/game/" + param;
            
            //api/game/posX|posY|nextX|nextY|figure
            var req = new XMLHttpRequest();
            req.open('GET', _url, false);
            req.send(null);

            if (req.status === 200) {

                resolve(req.response);
            }
            else {
                reject('Error. Status code: ' + req.status);
            }
        });
    }
}
