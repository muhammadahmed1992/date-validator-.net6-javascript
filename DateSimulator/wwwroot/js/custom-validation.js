const validationEngine = (() => {
    let validationObj = [
        {
            fieldId: 'CustomDate_EmployementStartDate',
            get value() {
                return document.getElementById(this.fieldId).value;
            },
            validationsToBePerformed: {
                isNotEmpty() {
                    return { result: Boolean(this.value), message: 'data-val-required' };
                },
                format() {
                    return {
                        result: /^(\d{2}) \/ (\d{4})$/.test(this.value), message: 'data-val-format'
                    };
                },
                validity() {
                    return {
                        result: /^(0[1-9]|1[0-2]) \/ (19|20)\d{2}$/.test(this.value), message: 'data-val-validity'
                    };
                }
            }
        },
        {
            fieldId: 'CustomDate_EmployementEndDate',
            get value() {
                return document.getElementById(this.fieldId).value;
            },
            validationsToBePerformed: {
                isNotEmpty() {
                    return { result: Boolean(this.value), message: 'data-val-required' };
                },
                format() {
                    return {
                        result: /^(\d{2}) \/ (\d{4})$/.test(this.value), message: 'data-val-format'
                    };
                },
                validity() {
                    return {
                        result: /^(0[1-9]|1[0-2]) \/ (19|20)\d{2}$/.test(this.value), message: 'data-val-validity'
                    };
                },
                comparer(startDate) {
                    if (!this.value || !startDate) return { result: true, message: '' }; // allow empty values to be handled by Required attribute

                    var startParts = startDate.split('/');
                    var endParts = this.value.split('/');

                    var startYear = parseInt(startParts[1], 10);
                    var endYear = parseInt(endParts[1], 10);
                    var startMonth = parseInt(startParts[0], 10);
                    var endMonth = parseInt(endParts[0], 10);

                    return { result: endYear > startYear || (endYear === startYear && endMonth > startMonth), message: 'data-val-comparer' };
                }
            }
        }
    ];

    const validate = () => {
        let validationResults = [];
        for (let current of validationObj) {
            let fieldValidations = current.validationsToBePerformed;
            for (let validation in fieldValidations) {
                let result;
                if (validation === 'comparer') {
                    const startDate = document.getElementById('CustomDate_EmployementStartDate').value;
                    result = fieldValidations[validation].call(current, startDate);
                } else {
                    result = fieldValidations[validation].call(current);
                }

                if (!result.result) {
                    validationResults.push({ field: current.fieldId, message: result.message });
                    break;
                }
            }
        }
        return validationResults;
    };

    return {
        isValid: validate
    };
})();
