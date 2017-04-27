function CommonUtils(){

}


CommonUtils.prototype.translateDays = function(day) {
    switch(day){
        case Constants.DAYS[1]:
            return 'Понеділок';
            break;
        case Constants.DAYS[2]:
            return 'Вівторок';
            break;
        case Constants.DAYS[3]:
            return 'Середа';
            break;
        case Constants.DAYS[4]:
            return 'Четвер';
            break;
        case Constants.DAYS[5]:
            return "П'ятниця";
            break;
        case Constants.DAYS[6]:
            return 'Субота';
            break;
        case Constants.DAYS[0]:
            return 'Неділя';
            break;
    }
};