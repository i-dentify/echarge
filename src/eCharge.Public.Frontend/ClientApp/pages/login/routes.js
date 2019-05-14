import makeRoute from 'lib/routing/generator';
import layout from 'components/layout';
import view from './view';

const routes = [
    makeRoute('/login', layout, view, false),
];

export default routes;
