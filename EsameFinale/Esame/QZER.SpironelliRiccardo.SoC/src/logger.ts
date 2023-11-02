import winston from 'winston'

export const logger = winston.createLogger({
    level: 'debug',
    format: winston.format.combine(
        winston.format.colorize(),
        winston.format.timestamp(),
        winston.format.printf(({timestamp, level, message, tag}) => {
            return `${timestamp} [${level}]${tag ? `[${tag}]` : ''}: ${message}`
        })
    ),
    transports: [
        new winston.transports.Console()
    ]
})