import log from 'loglevel';

const isDevEnvironment = process.env.NODE_ENV === 'development';
log.setLevel(isDevEnvironment ? 'debug' : 'warn');

export default log;
