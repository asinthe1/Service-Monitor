kendo.data.ObservableObject.prototype.toWebAPIReadyJSON = function () {
    var data = this.toJSON();
    for (var property in data) {
        if (data.hasOwnProperty(property)) {
            if (data[property] == null ||
                typeof (data[property]) == 'object' ||
                typeof (data[property]) == 'function') {
                if (typeof (data[property]) == 'object' &&
                    data[property] != null &&
                    data[property].constructor === new Date().constructor) {
                    if (data[property].constructor === new Date().constructor) {
                        data[property] = kendo.toString(data[property], 'MM/dd/yyyy hh:mm:ss tt');
                    } else {
                        data[property] = data[property].toJSON();
                    }
                } else {
                    delete data[property];
                }
            }
           
        }
    }
    return data;
}